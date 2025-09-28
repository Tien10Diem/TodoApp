<script lang="ts" setup>
import { buttonT } from '@/stores/btRandL'
import {ref} from 'vue' 
import axios from '@/lib/axios'
import { useRouter } from 'vue-router';

const bt = buttonT()
bt.setButtonText('Login');
const usname = ref<string>('');
const uspass = ref<string>('');



interface users {
  UserName : string,
  UserEmail: string,
  UserPassword: string
}

const user = ref<users>({
  UserName: '',
  UserEmail: '',
  UserPassword: ''
});

const router = useRouter();

async function registerU(user: users) {
  try {
    const res = await axios.post('/api/auth/register', user);
    console.log('register ok', res.status, res.data);
    bt.setButtonText('Login')
    await router.push('/');
  } catch (err) {
    console.error('registerU error', err);
  }
}

async function loginU(user: users) {
  try{
        const res = await axios.post('/api/auth/login', user);
        console.log('login ok', res.status, res.data);

        const token = res.data?.accessToken ?? res.data?.token;
        if(!token) throw new Error('Token is null');
        // luu vao localstorage 
        localStorage.setItem('accessToken', token);

        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        bt.setButtonText('Register');
        const profileRes = await axios.get('/api/auth/profile');
        console.log('Profile response:', profileRes.data);
        const userId = profileRes.data?.userId;
        localStorage.setItem('UserId',userId);
        console.log('Extracted userId:', userId);
        console.log('Navigating to:', `/home/${userId}`);
        await router.push(`/home/${userId}`);
    }
  catch (err) {
    console.error('login error', err);
  }
}

const check = async () => {
  if(usname.value && uspass.value){
      if(usname.value.includes('@') && usname.value.includes('.') ){
        user.value = {
          UserName:'',
          UserEmail:usname.value,
          UserPassword:uspass.value
        }
      }
      else{
        user.value = {
          UserName:usname.value,
          UserEmail:'',
          UserPassword:uspass.value
        }
      }
  }
  if(bt.buttonText=='Register'){
      try{
        await registerU(user.value)
      }
      catch(err){
        console.log(err)
      }
        
    }
  else{
      try{
        await loginU(user.value)
      }
      catch(err){
        console.log(err)
      }
  }
  
  
}
</script>
<template>
  <h1>Well come to TodoApp {{ bt.buttonText }} Page</h1>
  <br />
  <form @submit.prevent="check">
    <div class="mb-3 row">
      <label for="staticEmail" class="col-sm-2 col-form-label"> <b>Email or Username</b> </label>
      <div class="col-sm-10">
        <input
          v-model="usname"
          type="text"
          class="form-control-plaintext mail"
          id="staticEmail"
          placeholder="  email@example.com/ username"
        />
      </div>
    </div>
    <div class="mb-3 row">
      <label for="inputPassword" class="col-sm-2 col-form-label"><b>Password:</b> </label>
      <div class="col-sm-10">
        <input v-model="uspass" type="password" class="form-control pass" id="inputPassword" />
      </div>
    </div>
    <div class="mb-3 row">
      <button type="submit" class="bt" >{{ bt.buttonText }}</button>
    </div>
  </form>
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
