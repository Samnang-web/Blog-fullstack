<template>
  <div class="flex-1 p-4 md:p-10 bg-blue-50/50 overflow-scroll">
    <h1 class="text-2xl font-semibold mb-4">All Blogs</h1>

    <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 mb-6">
      <!-- Search -->
      <input
        v-model="searchTerm"
        type="text"
        placeholder="Search by title..."
        class="p-2 border rounded w-full sm:max-w-sm"
      />

      <!-- Category Filter -->
      <select v-model="selectedCategory" class="p-2 border rounded w-full sm:max-w-xs">
        <option value="">All Categories</option>
        <option v-for="category in uniqueCategories" :key="category" :value="category">
          {{ category }}
        </option>
      </select>

      <!-- Add Blog -->
      <button
        @click="goToAddBlog"
        class="flex items-center gap-2 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition"
      >
        <Icon icon="ic:baseline-add-circle-outline" width="20" height="20" />
        Add New Blog
      </button>
    </div>

    <!-- Blog Table -->
    <table class="w-full table-auto border-collapse text-left">
      <thead>
        <tr class="bg-gray-100">
          <th class="p-3 border">#</th>
          <th class="p-3 border">Title</th>
          <th class="p-3 border">Category</th>
          <th class="p-3 border">Published Date</th>
          <th class="p-3 border">Updated Date</th>
          <th class="p-3 border">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(blog, index) in paginatedBlogs"
          :key="blog.id"
          class="hover:bg-gray-50 transition"
        >
          <td class="p-3 border">{{ blog.id }}</td>
          <td class="p-3 border font-medium">{{ truncateText(blog.title, 30) }}</td>
          <td class="p-3 border font-medium">{{ blog.categoryName }}</td>
          <td class="p-3 border">{{ formatDate(blog.createdAt) }}</td>
          <td class="p-3 border">{{ formatDate(blog.updatedAt) }}</td>
          <td class="p-3 border">
            <button class="text-blue-600 hover:underline mr-3" @click="editBlog(blog.id)">
              Edit
            </button>
            <button class="text-red-600 hover:underline" @click="deleteBlog(blog.id)">
              Delete
            </button>
          </td>
        </tr>
        <tr v-if="paginatedBlogs.length === 0">
          <td colspan="6" class="p-3 text-center text-gray-500">No blogs found.</td>
        </tr>
      </tbody>
    </table>

    <!-- Pagination -->
    <div class="mt-6 flex gap-2 justify-center">
      <button
        @click="currentPage--"
        :disabled="currentPage === 1"
        class="px-3 py-1 border rounded disabled:opacity-50"
      >
        Prev
      </button>

      <span class="px-4 py-1 bg-primary-40 rounded"> {{ currentPage }}</span>

      <button
        @click="currentPage++"
        :disabled="currentPage >= Math.ceil(filteredBlogs.length / itemsPerPage)"
        class="px-3 py-1 border rounded disabled:opacity-50"
      >
        Next
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useRouter } from 'vue-router'
import { getBlog, deleteBlogId } from '../../Services/BlogService'
import type { Blog } from '../../Types/BlogInterface'
import { Icon } from '@iconify/vue'

const blogs = ref<Blog[]>([])
const searchTerm = ref('')
const selectedCategory = ref('')

const currentPage = ref(1)
const itemsPerPage = 20

const router = useRouter()

onMounted(async () => {
  try {
    blogs.value = await getBlog()
  } catch (error) {
    console.error('Failed to fetch blogs', error)
  }
})

const uniqueCategories = computed(() => {
  const categories = blogs.value.map(blog => blog.categoryName)
  return Array.from(new Set(categories))
})

const filteredBlogs = computed(() => {
  return blogs.value.filter(blog => {
    const matchesTitle = blog.title.toLowerCase().includes(searchTerm.value.toLowerCase())
    const matchesCategory = !selectedCategory.value || blog.categoryName === selectedCategory.value
    return matchesTitle && matchesCategory
  })
})

const paginatedBlogs = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage
  const end = start + itemsPerPage
  return filteredBlogs.value.slice(start, end)
})

const editBlog = (id: number) => {
  router.push(`/admin/edit/${id}`)
}

const deleteBlog = async (id: number) => {
  if (!confirm('Are you sure you want to delete this blog?')) return

  try {
    await deleteBlogId(id)
    blogs.value = blogs.value.filter(blog => blog.id !== id)
  } catch (error) {
    console.error('Failed to delete blog:', error)
    alert('Failed to delete blog. Please try again.')
  }
}

const goToAddBlog = () => {
  router.push('/admin/addblog')
}

const truncateText = (text: string, maxLength = 30): string => {
  return text.length <= maxLength ? text : text.slice(0, maxLength) + '…'
}

const formatDate = (dateString?: string): string => {
  if (!dateString) return 'N/A'
  const date = new Date(dateString)
  return date.toLocaleDateString(undefined, {
    year: 'numeric',
    month: 'short',
    day: 'numeric'
  })
}
</script>

<style scoped>
/* Optional: Add any scoped styles if needed */
</style>
