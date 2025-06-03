<template>
    <div class="flex justify-between item-center py-5 mx-8 sm:mx-20 xl:mx-32">
            <img :src="logo" alt="logo" class="w-8 sm:w-12">
        <!-- <ul class="flex gap-4 text-primary">
            <li><router-link to="/" class="text-white">Home</router-link></li>
            <li><router-link to="/Service" class="text-white">Service</router-link></li>
            <li><router-link to="/about" class="text-white">About</router-link> </li>
            <li><router-link to="/blogs" class="text-white">Blog</router-link></li>
            <li><router-link to="/contact" class="text-white">Contact</router-link></li>
        </ul> -->

        <button v-on:click="login" class="flex items-center gap-2 rounded-full text-sm cursor-pointer bg-primary text-white px-10 py-2.5">
            Login
            <Icon icon="ic:baseline-arrow-right-alt" width="24" height="24" />
        </button>
    </div>
</template>
<script setup lang="ts">

    import logo from '../assets/logo.png'
    import { Icon } from '@iconify/vue';
    import { useRouter } from 'vue-router';
    
    const router = useRouter()

    const login = () => {
        router.push("/login");
    }
</script>