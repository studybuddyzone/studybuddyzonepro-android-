// StudyBuddyZone service worker
// Purpose: satisfy PWA installability requirements (manifest + registered SW)
// and provide a minimal offline app-shell fallback for the local loader page.
// This does NOT cache the remote app (https://studybuddypro-psi.vercel.app),
// since that content is controlled by the live deployment, not this repo.

const CACHE_NAME = "studybuddyzone-shell-v1";
const APP_SHELL = [
  "./index.html",
  "./manifest.json",
  "./icons/icon-192.png",
  "./icons/icon-512.png"
];

self.addEventListener("install", (event) => {
  event.waitUntil(
    caches.open(CACHE_NAME).then((cache) => cache.addAll(APP_SHELL))
  );
  self.skipWaiting();
});

self.addEventListener("activate", (event) => {
  event.waitUntil(
    caches.keys().then((keys) =>
      Promise.all(
        keys
          .filter((key) => key !== CACHE_NAME)
          .map((key) => caches.delete(key))
      )
    )
  );
  self.clients.claim();
});

// Network-first for navigation requests, falling back to the cached
// app-shell loader page when the device is offline. All other requests
// (including calls to the live app domain) pass straight through to
// the network so we never serve stale third-party/app content.
self.addEventListener("fetch", (event) => {
  const request = event.request;

  if (request.mode === "navigate") {
    event.respondWith(
      fetch(request).catch(() => caches.match("./index.html"))
    );
    return;
  }

  if (new URL(request.url).origin === self.location.origin) {
    event.respondWith(
      caches.match(request).then((cached) => cached || fetch(request))
    );
  }
});
