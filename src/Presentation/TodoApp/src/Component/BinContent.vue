<script setup lang="ts">
import api from '@/lib/axios';
import { onMounted, ref } from 'vue';

const page = ref<number>(1);
const userid = localStorage.getItem("UserId")

interface job{
  jobId: number,
  jobName : string,
  jobCreateAt : Date,
  jobMembers : number,
  jobDateEnd: Date,
  jobDateStart: Date, 
  jobRemainingTime: number,
  jobStatus : string
}

const jobs = ref<job[]>([]);

const del = async(jobid:number)=> {
  try{
    const res = await api.delete(`/api/jobs/delete/${jobid}/${userid}`)
    jobs.value = res.data;
    console.log(jobs.value);
    alert("Finished!!!");
    await fetchJobs()
  }
  catch(error){
    console.log("soft delete erorr",error);
  }
  
}

const restore = async(jobid: number)=> {
    try{
        const res = await api.put(`api/jobs/restore/${jobid}/${userid}`);
        jobs.value = res.data
        console.log(jobs.value);
        await fetchJobs();
        alert("Finished!!!")
    }
    catch(erorr){
        console.log(erorr);
    }

}

const fetchJobs= (async ()=> {
  try{
    const res =await api.get(`/api/jobs/getbinpage?page=${page.value}&pageSize=5&userid=${userid}`);
    console.log('Full response:', res.data);
    console.log('Items:', res.data.items);
    console.log('Items length:', res.data.items.length);
    jobs.value = res.data.items
    console.log('Jobs after assignment:', jobs.value)
  }
  catch (e){
    console.error(e);
  }
})

onMounted (async()=>{
  await fetchJobs();
})

const prev =  async()=> {
  if(page.value > 1){
    page.value --; 
    await fetchJobs();
  }
};

const next = async ()=> {
  const res =await api.get(`/api/jobs/getbinpage?page=${page.value+1}&pageSize=5&userid=${userid}`);
  if(res.data.items.length>0){
    page.value ++; 
    await fetchJobs();
  }
};

</script>

<template>
  <ul class="list-group list-group-flush">
    <li v-for="job in jobs" :key = job.jobId  class="list-group-item">
      Name: {{ job.jobName }}  - Member: {{ job.jobMembers }} - Date Start: {{ job.jobDateStart }} - Date end: {{ job.jobDateEnd }} - Status: {{ job.jobStatus }} 
      <button type="button" @click="restore(job.jobId)" class="button">Restore</button>
      <button type="button" @click="del(job.jobId)" class="button">Delete</button>
    </li>
  </ul>
  <div class="pagesize">
    <button class="buttonpage" type = "button" @click="prev">prev</button>
    <button class="buttonpage" type = "button" @click="next">next</button>
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
