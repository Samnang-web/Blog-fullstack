<template>
  <div class="p-6 bg-gray-50 min-h-screen">
    <div
      class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6"
    >
      <div
        v-for="blog in props.blogs"
        :key="blog.id"
        class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-xl transition-shadow duration-300 flex flex-col cursor-pointer"
        @click="handleClick(blog)"
      >
        <img
          :src="getFullImageUrl(blog.imageUrl)"
          alt="Blog image"
          class="w-full h-48 object-cover object-center mx-auto"
        />
        <div class="p-4 flex flex-col flex-grow">
          <h3
            class="text-lg font-semibold mb-2 hover:text-blue-600 truncate"
            title="Click to read more"
          >
            {{ blog.title }}
          </h3>
          <p class="text-gray-600 text-sm mb-3 flex-grow line-clamp-3">
            {{ blog.description }}
          </p>
          <div class="text-gray-500 text-xs flex items-center mb-2">
            <Icon
              icon="mdi:account"
              width="18"
              height="18"
              class="inline-block mr-1"
            />
            {{ blog.authorName }}
          </div>
          <p class="text-gray-400 text-xs">
            Published: {{ formatDate(blog.createdAt) }}
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Icon } from "@iconify/vue";
import type { Blog } from "../Types/BlogInterface";
import { defineProps } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();
const props = defineProps<{ blogs: Blog[] }>();

function formatDate(dateStr: string) {
  return new Date(dateStr).toLocaleDateString();
}

function getFullImageUrl(imageUrl: string | null) {
  return imageUrl ? `https://blog-fullstack-w0jp.onrender.com${imageUrl}` : "";
}

function handleClick(blog: Blog) {
  router.push({ name: "BlogPages", params: { id: blog.id } });
}
</script>
