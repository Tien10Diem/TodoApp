<script setup lang="ts">
import DetailContent from '@/Component/DetailContent.vue'
import NavbarHome from '@/Component/navbarHome.vue'

defineProps<{
  id: string
}>()
</script>

<template>
    <NavbarHome />
    <DetailContent />
</template>

<style scoped></style>
