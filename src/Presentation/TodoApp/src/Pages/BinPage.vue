<script setup lang="ts">
import NavbarHome from '@/Component/navbarHome.vue';
import BinContent from '@/Component/BinContent.vue'

defineProps<{
  userid: string
}>()

</script>

<template>
    <NavbarHome/>
    <BinContent />
</template>

<style scoped></style>
