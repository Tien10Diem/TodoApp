<script lang="ts" setup>
import { buttonT } from '@/stores/btRandL';

const bt = buttonT();


</script>
<template>
    <h1>Well come to TodoApp {{ bt.buttonText }} Page</h1>
    <br>
    <div class="mb-3 row">
    <label for="staticEmail" class="col-sm-2 col-form-label"> <b>Email or Username</b> </label>
    <div class="col-sm-10">
      <input type="text" class="form-control-plaintext mail" id="staticEmail" placeholder="  email@example.com/ username">
    </div>
  </div>
  <div class="mb-3 row">
    <label for="inputPassword" class="col-sm-2 col-form-label"><b>Password:</b>  </label>
    <div class="col-sm-10">
      <input type="password" class="form-control pass" id="inputPassword">
    </div>
  </div>
  <div class="mb-3 row">
    <button type="button" class="bt" >{{bt.buttonText }}</button>
  </div>
</template>

<style scoped>
.mail {
  width: 80%;
  border: 1px solid #000000;
  border-radius: 5px;
}
.pass {
  width: 80%;
  border: 1px solid #000000;
  border-radius: 5px;
}

.bt {
  width: 7%;
  margin-left: 76%;
  margin-top: 15px;
  border-radius: 5px;
  background-color: #29cc41;
  border: 2px solid #000000;
} 
.bt:hover {
  background-color: #ffb507;
}
</style>