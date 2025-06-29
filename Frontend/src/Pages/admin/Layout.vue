<template>
  <div>
    <!-- Top Navbar -->
    <div class="flex items-center justify-between py-2 h-[70px] px-4 sm:px-12 border-b border-gray-200 bg-white shadow">
      <div class="flex items-center gap-4">
        <img :src="logo" alt="logo" class="w-8 sm:w-12 cursor-pointer" />
        <div class="hidden sm:flex flex-col leading-tight">
          <span class="font-semibold text-gray-800 text-sm">{{ user.name }}</span>
          <span class="text-xs text-gray-500">{{ user.email }}</span>
        </div>
      </div>

      <div class="flex items-center gap-4">
        <span class="text-sm hidden sm:inline font-bold text-gray-600 capitalize">{{ user.role }}</span>
        <!-- Avatar -->
        <div class="w-10 h-10 rounded-full bg-gradient-to-tr from-blue-500 to-purple-500 flex items-center justify-center text-white text-2xl font-semibold">
          {{ user.name.charAt(0).toUpperCase() }}
        </div>

        <!-- View Website Button -->
        <a
          href="/"
          target="_blank"
          class="ml-3 flex items-center text-sm px-5 py-2 border border-primary text-primary rounded-full hover:bg-primary hover:text-white transition"
        >
          <Icon icon="mdi:eye-outline" width="20" height="20" />
          <span class="ml-1">View Website</span>
        </a>
        <!-- Logout Button -->
        <button
          @click="logout"
          class="ml-3 flex items-center text-sm px-6 py-2 bg-primary text-white rounded-full hover:bg-primary/90"
        >
          <Icon icon="ic:baseline-arrow-right-alt" width="20" height="20" />
          <span class="ml-1">Logout</span>
        </button>
      </div>
    </div>

    <!-- Main layout -->
    <div class="flex h-[calc(100vh-70px)]">
      <SideBar />
      <router-view />
    </div>
  </div>
</template>

<script setup lang="ts">
import SideBar from '../../admin/SideBar.vue'
import { useRouter } from 'vue-router'
import { onMounted, ref } from 'vue'
import logo from '../../assets/logo.png'
import { Icon } from '@iconify/vue'
import type { currentUsers } from '../../Types/UserInterface'
import { GetCurrentUser } from '../../Services/UserService'

const router = useRouter()

onMounted(() => {
  const token = localStorage.getItem('token')
  if (!token) {
    router.push('/login')
  }

})

const logout = () => {
  localStorage.removeItem('token')
  router.push('/login')
}

const user = ref<currentUsers>({ name: "", email: "", role :"" });

onMounted(async () => {
  try {
    user.value = await GetCurrentUser();
  } catch (error) {
    console.error("Failed to fetch user info:", error);
  }
});
</script>
