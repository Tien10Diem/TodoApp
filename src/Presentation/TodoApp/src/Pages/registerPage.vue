<script lang="ts" setup>
import NavbarLogin from '@/Component/navbarLandR.vue'
import RegisterContent from '@/Component/registerContent.vue'

defineProps<{
    userid: number;
}>()
</script>
<template>
    <NavbarLogin />
    <RegisterContent/>

</template>

<style scoped></style>