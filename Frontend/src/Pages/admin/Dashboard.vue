<template>
  <div class="flex-1 p-4 md:p-10 bg-blue-50/50">
    <!-- Welcome Section -->
    <div class="bg-gradient-to-r from-blue-400 to-blue-500 text-white shadow-lg rounded-xl p-6 flex items-center space-x-6">
      <!-- Profile Icon / Avatar -->
      <div class="flex-shrink-0">
        <div class="w-16 h-16 rounded-full bg-gradient-to-tr from-blue-500 to-purple-500 flex items-center justify-center text-white text-2xl font-semibold">
          {{ user.name.charAt(0).toUpperCase() }}
        </div>
      </div>

    <!-- Text Content -->
    <div>
      <h1 class="text-2xl font-semibold text-gray-900">Welcome, <span class="font-bold uppercase">{{ user.name }}</span> 👋</h1>
      <p class="text-1xl font-semibold text-gray-800 mt-1">You are logged in as <span class="font-bold uppercase">{{ user.role }}</span>.</p>
    </div>
  </div>

    <!-- Stats Section -->
    <div class="grid grid-cols-1 md:grid-cols-3 gap-6 mt-4">
      <!-- Users -->
      <div
        class="bg-white p-6 rounded-md shadow hover:shadow-lg transition duration-300 flex flex-col items-center text-center space-y-2"
      >
        <Icon icon="line-md:account-small" class="size-10 text-indigo-500" />
        <p class="text-lg font-semibold">{{ usersCount }} All Users</p>
      </div>

      <!-- Blogs -->
      <div
        class="bg-white p-6 rounded-md shadow hover:shadow-lg transition duration-300 flex flex-col items-center text-center space-y-2"
      >
        <Icon icon="material-symbols-light:blood-pressure-outline-rounded" class="size-10 text-lime-500" />
        <p class="text-lg font-semibold">{{ blogsCount }} Blogs</p>
      </div>

      <!-- Admins -->
      <div
        class="bg-white p-6 rounded-md shadow hover:shadow-lg transition duration-300 flex flex-col items-center text-center space-y-2"
      >
        <Icon icon="ic:outline-admin-panel-settings" class="size-10 text-red-500" />
        <p class="text-lg font-semibold">{{ adminsCount }} Admins</p>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { Icon } from '@iconify/vue'
import { getBlog } from '../../Services/BlogService'  // adjust the path if needed
import { GetAllUsers, GetCurrentUser } from '../../Services/UserService'
import type { currentUsers } from '../../Types/UserInterface'

const usersCount = ref(0)     // static example, replace if you want dynamic
const adminsCount = ref(0)    // static example, replace if you want dynamic
const blogsCount = ref(0)

async function fetchDashboardData() {
  try {
    const blogs = await getBlog()
    blogsCount.value = blogs.length

    const users = await GetAllUsers()
    usersCount.value = users.length

    adminsCount.value = users.filter(user => user.role.toLowerCase() === 'admin').length

  } catch (error) {
    console.error('Failed to fetch blogs:', error)
  }
}

onMounted(() => {
  fetchDashboardData()
})

const user = ref<currentUsers>({ name: "", email: "", role :"" });

onMounted(async () => {
  try {
    user.value = await GetCurrentUser();
  } catch (error) {
    console.error("Failed to fetch user info:", error);
  }
});
</script>
