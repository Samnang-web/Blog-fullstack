<template>
  <div class="flex flex-col border-r border-gray-200 min-h-full pt-6">

    <router-link
        to="/admin"
        :class="['flex items-center space-x-2', linkClass('/admin')]"
      >
        <Icon icon="material-symbols:dashboard-outline" width="24" height="24" /> 
        <span>Dashboard</span>
      </router-link>

      <!-- <router-link
        to="/admin/profile"
        :class="['flex items-center space-x-2', linkClass('/admin/profile')]"
      >
        <Icon icon="material-symbols:dashboard-outline" width="24" height="24" /> 
        <span>Profile</span>
      </router-link> -->

      <router-link
          to="/admin/listblog"
          :class="['flex items-center space-x-2' ,linkClass('/admin/listblog')]"
        >
          <Icon icon="mdi:file-document-box-multiple-outline" width="24" height="24" />
          <span>Blog Lists</span>
      </router-link>

      <router-link
        to="/admin/addblog"
        :class="['flex items-center space-x-2', linkClass('/admin/addblog')]"
      >
        <Icon icon="ic:baseline-add-circle-outline" width="24" height="24" />
        <span>Add Blog</span>
      </router-link>

      <router-link
        to="/admin/users"
        :class="['flex items-center space-x-2', linkClass('/admin/users')]"
      >
        <Icon icon="mdi:user-group" width="24" height="24" />
        <span>Users</span>
      </router-link>
    </div>
</template>

<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router'
import { Icon } from '@iconify/vue'
import { onMounted } from 'vue'

const route = useRoute()
const router  = useRouter()


onMounted(() => {
  const token = localStorage.getItem('token')
  if (!token) {
    router.push('/login')
  }
})


const linkClass = (path: string): string[] => {
  return [
    'flex items-center gap-3 py-3.5 px-3 md:px-9 md:min-w-64 cursor-pointer',
    route.path === path
      ? 'bg-primary-10 text-blue-700 border-r-4 border-primary'
      : 'text-gray-700 hover:bg-gray-100'
  ]
}
</script>
