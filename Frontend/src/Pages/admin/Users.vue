<template>
  <div class="flex-1 p-4 md:p-10 bg-blue-50/50 overflow-scroll">
    <h3 class="text-xl font-semibold mb-4">Add New User</h3>
    <form @submit.prevent="handleAddUser" class="mb-6 flex gap-6 flex-wrap">
      <input v-model="newUser.name" placeholder="Name" class="border p-2 rounded" />
      <input v-model="newUser.email" placeholder="Email" class="border p-2 rounded" />
      <input v-model="newUser.password" placeholder="Password" type="password" class="border p-2 rounded" />
      <select v-model="newUser.role" class="border p-2 rounded">
        <option>Admin</option>
        <option>Editor</option>
        <option>Author</option>
      </select>
      <button type="submit" class="bg-green-500 text-white px-4 py-2 rounded">Add User</button>
    </form>

    <h1 class="text-xl font-semibold mb-4">All Users</h1>
    <table class="w-full table-auto border-collapse text-left">
      <thead>
        <tr class="bg-gray-100">
          <th class="p-3 border">No</th>
          <th class="p-3 border">Name</th>
          <th class="p-3 border">Email</th>
          <th class="p-3 border">Role</th>
          <th class="p-3 border">Action</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(user, index) in users"
          :key="user.id"
          class="hover:bg-gray-50 transition"
        >
          <td class="p-3 border text-center">{{ index + 1 }}</td>
          <td class="p-3 border">{{ user.name }}</td>
          <td class="p-3 border">{{ user.email }}</td>
          <td class="p-3 border">{{ user.role }}</td>
          <td class="p-3 border">
            <button @click="handleDeleteUser(user.id)" class="bg-red-500 text-white px-3 py-1 rounded">
              Delete
            </button>
          </td>
        </tr>
        <tr v-if="users.length === 0">
          <td colspan="5" class="p-3 text-center text-gray-500">Show For Admin Only!</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { GetAllUsers, deleteUser, addUser } from '../../Services/UserService'
import type { Users } from '../../Types/UserInterface'

const users = ref<Users[]>([])
const newUser = ref<Users>({
  id: 0,
  name: '',
  email: '',
  password: '',
  role: 'Admin'
})

const loadUsers = async () => {
  users.value = await GetAllUsers()
}

const handleAddUser = async () => {
  try {
    await addUser(newUser.value)
    await loadUsers()
    newUser.value = { id: 0, name: '', email: '', password: '', role: 'Admin' }
  } catch (error) {
    console.error('Add user failed:', error)
  }
}

const handleDeleteUser = async (id: number) => {
  if (confirm('Are you sure you want to delete this user?')) {
    try {
      await deleteUser(id)
      await loadUsers()
    } catch (error: any) {
      console.error('Delete user failed:', error.response?.data || error.message)
    }
  }
}

onMounted(loadUsers)
</script>
