<template>
  <div
    class="min-h-screen flex items-center justify-center bg-gradient-to-br from-blue-200 to-indigo-300"
  >
    <div class="bg-white shadow-md rounded-lg p-8 w-full max-w-md space-y-6">
      <div class="w-full py-6 text-center">
        <h1 class="text-3xl font-bold"><span class="text-primary">Admin</span> Login</h1>
        <p class="font-light">Enter your credentials to access the Admin panel</p>
      </div>

      <form @submit.prevent="handleLogin" class="space-y-4">
        <!-- Email -->
        <div class="flex flex-col">
          <label class="block text-gray-700 mb-1">Email</label>
          <input
            v-model="email"
            type="email"
            class="border-b-2 border-gray-300 p-2 outline-none mb-6"
            placeholder="Enter your email"
            required
          />
        </div>

        <!-- Password -->
        <div class="flex flex-col">
          <label class="block text-gray-700 mb-1">Password</label>
          <input
            v-model="password"
            type="password"
            class="border-b-2 border-gray-300 p-2 outline-none mb-6"
            placeholder="Enter your password"
            required
          />
        </div>
        <!-- Submit Button -->
        <button
          type="submit"
          class="w-full bg-indigo-600 text-white py-2 rounded-md hover:bg-indigo-700 transition"
          :disabled="loading"
        >
          Login
        </button>
        <p v-if="error" class="text-red-600 mt-2 text-center">{{ error }}</p>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { login } from "../Services/AuthService";
import { useRouter, useRoute } from "vue-router";

const email = ref("");
const password = ref("");
const error = ref<string | null>(null);
const loading = ref(false);

const router = useRouter();
const route = useRoute();

async function handleLogin() {
  error.value = null;
  loading.value = true;

  try {
    const user = await login({ email: email.value, password: password.value });
    localStorage.setItem("token", user.token);

    const redirectPath = (route.query.redirect as string) || "/admin";
    router.push(redirectPath);
  } catch (err: any) {
    error.value = err.response?.data?.message || err.message || "Login failed";
  } finally {
    loading.value = false;
  }
}
</script>
