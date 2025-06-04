<template>
  <div class="flex-1 p-4 md:p-10 bg-blue-50/50">
    <!-- Welcome Section -->
    <h3 class="text-xl font-semibold mb-4">Add New Category</h3>
    <form @submit.prevent="handleAddCategory" class="mb-6 flex gap-6 flex-wrap">
      <input v-model="newCategory.name" placeholder="Category Name" class="border p-2 rounded" />
      <button type="submit" class="bg-green-500 text-white px-4 py-2 rounded">Add Category</button>
    </form>

    <h1 class="text-xl font-semibold mb-4">All Users</h1>
    <table class="w-full table-auto border-collapse text-left">
      <thead>
        <tr class="bg-gray-100">
          <th class="p-3 border">#</th>
          <th class="p-3 border">Name</th>
          <th class="p-3 border">Created Date</th>
          <th class="p-3 border">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(cate, index) in category"
          :key="cate.id"
          class="hover:bg-gray-50 transition"
        >
          <td class="p-3 border text-center">{{ index + 1 }}</td>
          <td class="p-3 border">{{ cate.name }}</td>
          <td class="p-3 border">{{ formatDate(cate.createdAt) }}</td>
           <td class="p-3 border">
            <button @click="handleDeleteCategory(cate.id)" class="bg-red-500 text-white px-3 py-1 rounded">
              Delete
            </button>
          </td>
        </tr>
        <!-- <tr v-if="users.length === 0">
          <td colspan="5" class="p-3 text-center text-gray-500">Show For Admin Only!</td>
        </tr> -->
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import type { Category } from '../../Types/CategoryInterface'
import { addCategory, deleteCategory, getCategories } from '../../Services/CategoryService'

const category = ref<Category[]>([])
const newCategory = ref<Category>({
  id: 0,
  name: '',
  createdAt: '',
})

const loadCategories = async () => {
  category.value = await getCategories()
}


const handleAddCategory = async () => {
  try {
    await addCategory(newCategory.value.name)
    await loadCategories()
    newCategory.value = { id: 0, name: '', createdAt: '' }
  } catch (error) {
    console.error('Add category failed:', error)
  }
}

const handleDeleteCategory = async (id: number) => {
  if (confirm('Are you sure you want to delete this Category?')) {
    try {
      await deleteCategory(id)
      await loadCategories()
    } catch (error: any) {
      console.error('Delete Category failed:', error.response?.data || error.message)
    }
  }
}

const formatDate = (dateStr: string) => {
  const date = new Date(dateStr)
  return date.toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
  })
}

onMounted(() => {
  loadCategories()
})
</script>
