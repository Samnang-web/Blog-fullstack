<template>
  <section class="p-6 max-w-3xl mx-auto">
    <div class="bg-white rounded shadow p-6">
      <h2 class="text-2xl font-semibold mb-6">User Profile</h2>
      <div class="flex flex-col items-center gap-4">
        <img
          :src="user.avatar"
          alt="Profile"
          class="w-24 h-24 rounded-full border-2 border-gray-300"
        />

        <div class="w-full">
          <div class="mb-4">
            <label class="block text-sm font-medium text-gray-600">Name</label>
            <input
              type="text"
              :value="user.name"
              readonly
              class="w-full mt-1 border border-gray-300 rounded px-3 py-2 bg-gray-100"
            />
          </div>

          <div class="mb-4">
            <label class="block text-sm font-medium text-gray-600">Email</label>
            <input
              type="email"
              :value="user.email"
              readonly
              class="w-full mt-1 border border-gray-300 rounded px-3 py-2 bg-gray-100"
            />
          </div>

          <div class="mb-4">
            <label class="block text-sm font-medium text-gray-600">Role</label>
            <input
              type="text"
              :value="user.role"
              readonly
              class="w-full mt-1 border border-gray-300 rounded px-3 py-2 bg-gray-100"
            />
          </div>

          <div v-if="user.role === 'admin'" class="text-right">
            <button
              class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
              @click="editUser"
            >
              Edit Profile
            </button>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script lang="ts">
import { defineComponent, reactive } from 'vue'

export default defineComponent({
  name: 'UserProfile',
  setup() {
    // Simulated logged-in user info
    const user = reactive({
      name: 'John Doe',
      email: 'john@example.com',
      role: 'user', // or 'admin'
      avatar: 'https://avatars.dicebear.com/v2/initials/john-doe.svg',
    })

    const editUser = () => {
      // Redirect or show editable form (you can customize this)
      alert('Redirecting to admin edit form...')
    }

    return {
      user,
      editUser,
    }
  },
})
</script>

<style scoped>
input[readonly] {
  cursor: not-allowed;
}
</style>
