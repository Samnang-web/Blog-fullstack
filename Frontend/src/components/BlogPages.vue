<template>
  <div v-if="blog" class="relative">
    <!-- <img :src="getFullImageUrl(blog.imageUrl)" class="w-full rounded mb-4" /> -->

    <div class="text-center mt-20 text-gray-600">
      <p class="text-primary py-4 font-medium">
        Published On: {{ formatDate(blog.createdAt) }}
      </p>
      <h1
        class="text-2xl sm:text-5xl font-semibold max-w-2xl mx-auto text-gray-800"
      >
        {{ blog.title }}
      </h1>
      <h2 class="my-5 max-w-lg truncate mx-auto">{{ blog.content }}</h2>
      <p
        class="inline-block py-1 px-4 rounded-full mb-6 text-sm border-primary-40 bg-primary-10 font-medium text-primary"
      >
        {{ blog.authorName }}
      </p>
    </div>
    <div class="mx-5 max-w-3xl md:mx-auto my-10 mt-6">
      <img
        :src="getFullImageUrl(blog.imageUrl)"
        alt="Blog image"
        class="rounded-3xl mb-5"
        loading="lazy"
      />
      <div class="max-w-3xl mx-auto">
        {{ blog.description }}
      </div>
    </div>
  </div>
  <div v-else class="text-center text-gray-500">Loading...</div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import type { Blog } from "../Types/BlogInterface";
import { getBlogById } from "../Services/BlogService";

const route = useRoute();
const blog = ref<Blog | null>(null);

onMounted(async () => {
  const id = Number(route.params.id);
  blog.value = await getBlogById(id);
});

function getFullImageUrl(imageUrl: string | null) {
  if (!imageUrl) return "/fallback.jpg";

  if (imageUrl.startsWith("http://") || imageUrl.startsWith("https://")) {
    if (imageUrl.includes("res.cloudinary.com")) {
      // Cloudinary image: add transformation (resize, quality, format)
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
</script>
