<template>
  <div class="flex-1 p-4 ml-4 md:p-10 bg-blue-50/50 overflow-scroll items-center">
    <h1 class="text-3xl font-bold mb-4 ml-10">Update Blog</h1>
    <form @submit.prevent="handleUpdateBlog" class=" bg-blue-50/50 text-gray-600  h-full">
      <div class="bg-white w-full max-w-4xl p-4 md:p-10 sm:m-10 shadow rounded">

      <div>
        <label class="block text-lg font-medium mb-2 mt-4">Blog Title</label>
        <input 
          type="text"
          class="w-full max-w-lg mt-2 p-2 border border-gray-300 outline-none rounded focus:outline-none focus:ring-2 focus:ring-blue-300"
          v-model="blog.title"
          required
        />
      </div>

      <!-- <div>
        <label class="block mb-1 font-medium">Category</label>
        <input
          v-model="blog.categoryName"
          type="text"
          class="w-full border border-gray-300 rounded-md px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-400"
          required
        />
      </div> -->

      <div>
        <label class="block text-lg font-medium mb-2 mt-4">Sub Title</label>
        <textarea 
          rows="2"
          class="w-full max-w-lg mt-2 p-2 border border-gray-300 outline-none rounded focus:outline-none focus:ring-2 focus:ring-blue-300"
          v-model="blog.content"
          required
        ></textarea>
      </div>

      <div>
        <label class="block text-lg font-medium mb-2 mt-4">Description</label>
        <textarea 
          rows="8"
          class="w-full bg-white px-4 py-3 rounded-md border border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-300"
          v-model="blog.description"
          required
        ></textarea>
      </div>

      <div>
        <label class="block text-lg font-medium mb-2 mt-4">Author Name</label>
        <input 
          type="text"
          class="w-full max-w-lg mt-2 p-2 border border-gray-300 outline-none rounded focus:outline-none focus:ring-2 focus:ring-blue-300"
          v-model="blog.authorName"
          required
        />
      </div>

      <div class="pt-4">
        <button
          type="submit"
          class="bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 transition"
        >
          Update Blog
        </button>
      </div>
    </div>
    </form>
  </div>
</template>


<script setup lang="ts">
import { reactive, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { getBlogById, updateBlog as updateBlogApi } from '../../Services/BlogService'
import type { Blog } from '../../Types/BlogInterface'

const route = useRoute()
const router = useRouter()
const blogId = Number(route.params.id)

const blog = reactive<Blog>({
  id: 0,
  title: '',
  description: '',
  content: '',
  authorName: '',
  imageUrl: '',   
  createdAt: '',    
  updatedAt: '',      
  categoryName: '',
});

onMounted(async () => {
  try {
    const response = await getBlogById(blogId)
    Object.assign(blog, response)
  } catch (error) {
    alert('Blog not found')
    router.push('/admin/listblog')
  }
})

const handleUpdateBlog = async () => {
  try {
    await updateBlogApi(blogId, blog)
    alert('Blog updated successfully!')
    router.push('/admin/listblog')
  } catch (error) {
    alert('Failed to update blog.')
    console.error(error)
  }
}
</script>
