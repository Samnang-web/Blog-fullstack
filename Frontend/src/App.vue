<script setup lang="ts">
    import Navbar from './components/Navbar.vue';
    import Footer from './components/Footer.vue';
    import { useRoute } from 'vue-router';
    import { computed } from 'vue';

    const route = useRoute();

    // Define routes where Navbar and Footer should be hidden
    const hideLayout = computed(() => {
        const hiddenPaths = ['/admin', '/login'];
        return hiddenPaths.some(path => route.path.startsWith(path));
    });
</script>

<template>
    <Navbar v-if="!hideLayout"/>
    <router-view/>
    <Footer v-if="!hideLayout"/>
</template>
