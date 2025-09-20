<script setup lang="ts">


</script>

<template>
  <ul class="list-group list-group-flush">
    <li class="list-group-item">
      Name:  - Member:  -  - 
      <button class="button">Details</button>
      <button type="button"  class="button">Delete</button>
    </li>
  </ul>
  <div class="pagesize">
    <button class="buttonpage">prev</button>
    <button class="buttonpage">next</button>
  </div>
</template>

<style>
.button{
  margin-left: 10px;
  background-color: rgb(114, 203, 20);
  border-radius: 8%;
}
.pagesize {
    margin-top: 15px;

}
.buttonpage{
    background-color: rgb(114, 203, 20);
    margin-left: 80px;
    border-radius: 12%;
}

.buttonpage:hover{
    background-color: rgb(99, 99, 99);
}

.button:hover{
  background-color: rgb(99, 99, 99);
}
</style>
