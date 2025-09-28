<script setup lang="ts">
import { useRoute } from 'vue-router';
import { onMounted, ref } from 'vue';
import api from '@/lib/axios';

const route = useRoute()
const id = route.params.id as string

console.log('Route params:', route.params);
console.log('ID:', id);

if (!id) {
  console.error('No ID found in route params');

}

interface jobs{
  jobId: number,
  jobName : string,
  jobCreateAt : Date,
  jobMembers : number,
  jobDateEnd: Date,
  jobDateStart: Date, 
  jobRemainingTime: number,
  jobStatus : string
}

const datestart = ref<Date>();
const dateend = ref<Date>();
const job = ref<jobs | null>(null);

const Ujob = async()=> {
  try{
      const res = await api.put(`/api/jobs/updateJob/${id}`,{
      jobId: Number(id),
      jobName: job.value?.jobName,
      jobMembers: job.value?.jobMembers,
      jobDateStart: datestart.value,
      jobDateEnd: dateend.value,
      jobRemainingTime: job.value?.jobRemainingTime,
      jobStatus: job.value?.jobStatus
    })

    job.value =res.data
    await fetchJobs();
    console.log(job.value)
    alert("Finish!!!")
  }
  catch (error) {
      console.error('Update failed:', error);
  }
}
const fetchJobs = async ()=>{
    try{
        const res = await api.get(`/api/jobs/by-id/${id}`)
        job.value = res.data
        datestart.value = job.value?.jobDateStart
        dateend.value= job.value?.jobDateEnd
        console.log(job.value)
    }
    catch{
        throw new MessageEvent("Erorr load data");
    }
}

onMounted(fetchJobs)
</script>

<template>
  <form  @submit.prevent="Ujob">
    <div class="mb-3">
      <label for="FullName" class="form-label">Name Job</label>
      <div v-if="job">
        <input
          v-model="job.jobName"
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
      <div v-if="job">
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
      <div v-if="job">
        <input
          v-model="dateend"
          type="date"
          class="form-control"
          id="FullName"
          aria-describedby="name"
          placeholder="yyyy-MM-dd"
        />
      </div>
    </div>
    <div class="mb-3">
      <label for="members" class="form-label">Number of members</label>
      <div v-if="job">
        <input v-model="job.jobMembers" type="number" class="form-control" id="members" placeholder="5"/>
      </div>
      
    </div>
    <div class="mb-3">
      <label for="Status" class="form-label">Status</label>
        <div v-if="job">
        <select v-model="job.jobStatus">
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
