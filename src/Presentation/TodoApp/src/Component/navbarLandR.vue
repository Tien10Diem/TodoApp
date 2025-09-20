<script lang="ts" setup>
import { buttonT } from '@/stores/btRandL';
import { RouterLink } from 'vue-router';
const bt = buttonT();

</script>
<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light nav">
    <div class="container-fluid">
      <a class="navbar-brand" href="#">Navbar</a>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarNavAltMarkup"
        aria-controls="navbarNavAltMarkup"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
        <div class="navbar-nav">
          <RouterLink to="/register" class="nav-link active res" @click="bt.setButtonText('Register')">
            Register
          </RouterLink>

          <RouterLink to="/login" class="nav-link active log" @click="bt.setButtonText('Login')">
            Login
          </RouterLink>
        </div>
      </div>
    </div>
  </nav>
</template>

<style scoped>
.nav {
  background-color: #29cc41 !important;
  margin-bottom: 50px;
}
.res {
  margin-left: 20%;
  margin-right: 20%;
  border-radius: 15px;
  background-color:#bbfa3d ;
}
.log {
  border-radius: 15px;
  background-color: #bbfa3d;
}

.res:hover {
  background-color: #ffb507;
}
.log:hover {
  background-color: #ffb507;
}


</style>

