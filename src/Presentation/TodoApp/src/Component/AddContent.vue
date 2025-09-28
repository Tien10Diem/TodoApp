<script setup lang="ts">
import api from '@/lib/axios';
import router from '@/router';
import { ref } from 'vue';

const userid = localStorage.getItem('UserId');

const datestart = ref<string>('');
const dateend = ref<string>('');

const jobs = ref({
   jobName: "",
   jobCreateAt: new Date(),
   jobMembers: 1,
   jobDateEnd: new Date(), 
   jobDateStart: new Date(),
   jobRemainingTime: new Date(),
   jobStatus: "Pause",
   UserId: Number(userid)
});

const add = async()=> {
    try{
       // Update dates từ form trước khi gửi
       jobs.value.jobDateStart = datestart.value ? new Date(datestart.value) : new Date();
       jobs.value.jobDateEnd = dateend.value ? new Date(dateend.value) : new Date();
       
       console.log('userid:', userid, 'type:', typeof userid);
       console.log('UserId converted:', Number(userid));
       console.log('Sending data:', JSON.stringify(jobs.value, null, 2));
       
       const res = await api.post(`/api/jobs/addjob`, jobs.value);
       console.log('Job created:', res.data); 
       router.back()
    }
    catch (error){
        console.log(error)
    }
}

</script>

<template>
  <form  @submit.prevent="add">
    <div class="mb-3">
      <label for="FullName" class="form-label">Name Job</label>
      <div v-if="jobs">
        <input
          v-model="jobs.jobName"
          type="Text"
          class="form-control"
          id="FullName"
          aria-describedby="name"
          placeholder="ABCXYZ"
        />
      </div>
      <div id="emailHelp" class="form-text">Full name</div>
    </div>
    <div class="mb-3">
      <label for="FullName" class="form-label">Date Start</label>
      <div v-if="jobs">
        <input
          v-model="datestart"
          type="date"
          class="form-control"
          id="FullName"
          aria-describedby="name"
          placeholder="yyyy-MM-dd"
        />
      </div>
    </div>
    <div class="mb-3">
      <label for="FullName" class="form-label">Date end</label>
      <input
        v-model="dateend"
        type="date"
        class="form-control"
        id="FullName"
        aria-describedby="name"
        placeholder="yyyy-MM-dd"
      />
    </div>
    <div class="mb-3">
      <label for="members" class="form-label">Number of members</label>
      <div v-if="jobs">
        <input v-model="jobs.jobMembers" type="number" class="form-control" id="members" placeholder="5"/>
      </div>
      
    </div>
    <div class="mb-3">
      <label for="Status" class="form-label">Status</label>
        <div v-if="jobs">
        <select v-model="jobs.jobStatus">
          <option>completed</option>

          <option>Unfinish</option>

          <option>Pause</option>
        </select>
      </div>
    </div>
    <button type="button" class="btn btn-primary back" @click="$router.back()">Back</button>

    <button type="submit" class="btn btn-primary submit">Submit</button>

  </form>
</template>

<style scoped>
.back {
  margin-left: 100px;
}

.submit {
  margin-left: 1050px;
}
</style>
