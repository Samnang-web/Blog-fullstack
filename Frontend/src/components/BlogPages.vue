<template>
  <div v-if="blog" class="relative max-w-4xl mx-auto px-4 py-12">
    <button
      @click="goBack"
      class="mb-8 inline-flex items-center px-6 py-2 bg-gray-300 hover:bg-blue-400 rounded-md text-blue-700 transition"
    >
      <Icon icon="material-symbols:keyboard-backspace-rounded" width="26" height="26"  style="color: black" />
        Back
    </button>

    <div class="text-center text-gray-600 mb-8">
      <p class="text-blue-600 font-semibold mb-2">
        Published On: {{ formatDate(blog.createdAt) }}
      </p>
      <h1
        class="text-3xl sm:text-5xl font-bold max-w-3xl mx-auto text-gray-900 mb-4"
      >
        {{ blog.title }}
      </h1>
      <h2
        class="text-lg text-gray-700 max-w-2xl mx-auto mb-6 whitespace-pre-line"
      >
        {{ blog.content }}
      </h2>
      <p
        class="inline-block py-1 px-4 rounded-full mb-6 text-sm border border-blue-300 bg-blue-50 text-blue-700 font-semibold"
      >
        {{ blog.authorName }}
      </p>
    </div>

    <div class="mx-5 md:mx-auto max-w-3xl">
      <img
        :src="getFullImageUrl(blog.imageUrl)"
        alt="Blog image"
        class="rounded-3xl mb-8 w-full object-cover"
        loading="lazy"
      />
      <div class="prose prose-orange max-w-none text-gray-700">
        {{ blog.description }}
      </div>
    </div>
  </div>

  <div v-else class="text-center text-gray-500 py-12">Loading...</div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import type { Blog } from "../Types/BlogInterface";
import { getBlogById } from "../Services/BlogService";
import { Icon } from "@iconify/vue";

const route = useRoute();
const router = useRouter();
const blog = ref<Blog | null>(null);

onMounted(async () => {
  const id = Number(route.params.id);
  blog.value = await getBlogById(id);
});

function getFullImageUrl(imageUrl: string | null) {
  if (!imageUrl) return "/fallback.jpg";

  if (imageUrl.startsWith("http://") || imageUrl.startsWith("https://")) {
    if (imageUrl.includes("res.cloudinary.com")) {
      return imageUrl.replace(
        "/upload/",
        "/upload/w_800,h_500,c_fill,q_auto,f_auto/"
      );
    }
    return imageUrl;
  }

  return `https://blog-fullstack-w0jp.onrender.com${imageUrl}`;
}

function formatDate(dateStr: string | undefined) {
  return dateStr ? new Date(dateStr).toLocaleDateString() : "";
}

// Back button handler
function goBack() {
  router.back();
}
</script>
