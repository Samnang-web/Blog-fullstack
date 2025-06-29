<template>
  <div class="mx-8 sm:mx-16 xl:mx-24 relative">
    <div class="text-center mt-20 mb-8">
      <div
        class="inline-flex items-center justify-center gap-4 px-6 py-1.5 mb-4 border border-primary-40 bg-primary-10 rounded-full text-sm text-primary"
        role="status"
        aria-label="Blog label"
      >
        <p>Happy blog</p>
      </div>
      <h1
        class="text-3xl sm:text-6xl font-semibold sm:leading-[4rem] text-gray-700"
      >
        Your own <span class="text-primary">Blogging</span> <br />
        platform
      </h1>
      <p
        class="my-6 sm:my-8 max-w-2xl mx-auto max-sm:text-xs text-gray-500 leading-relaxed"
      >
        Lorem Ipsum is simply dummy text of the printing and typesetting
        industry. Lorem Ipsum has been the industry's standard dummy text ever
        since the 1500s,
      </p>
    </div>

    <nav
      class="flex flex-wrap gap-3 justify-center mb-6"
      aria-label="Blog categories"
    >
      <button
        v-for="cat in categories"
        :key="cat"
        @click="selectedCategory = cat"
        :class="[
          'px-4 py-2 rounded-full border transition focus:outline-none focus:ring-2 focus:ring-offset-1 focus:ring-primary',
          selectedCategory === cat
            ? 'bg-blue-600 text-white border-blue-600'
            : 'bg-white text-gray-700 border-gray-300 hover:bg-gray-100'
        ]"
        :aria-pressed="selectedCategory === cat ? 'true' : 'false'"
      >
        {{ cat }}
      </button>
    </nav>

    <!-- Show filtered blogs, if you want to limit to first 8, slice it in BlogCard or here -->
    <BlogCard :blogs="filteredBlogs" />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from "vue";
import { getBlog } from "../Services/BlogService";
import type { Blog } from "../Types/BlogInterface";
import BlogCard from "../components/BlogCard.vue";

const blogs = ref<Blog[]>([]);
const selectedCategory = ref("All");

// Fetch blogs
onMounted(async () => {
  blogs.value = await getBlog();
});

// Extract unique categories from blogs + "All"
const categories = computed(() => {
  const cats = blogs.value.map((b) => b.categoryName || "Uncategorized");
  return ["All", ...new Set(cats)];
});

// Filter by selected category
const filteredBlogs = computed(() => {
  if (selectedCategory.value === "All") return blogs.value;
  return blogs.value.filter((b) => b.categoryName === selectedCategory.value);
});
</script>
