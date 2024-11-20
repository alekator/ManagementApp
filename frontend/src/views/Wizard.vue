<template>
  <div>
    <h1>Project Creation Wizard</h1>
    <div v-if="step === 1">
      <h2>Step 1: Project Details</h2>
      <form @submit.prevent="goToNextStep">
        <label>
          Project Name:
          <input v-model="project.name" required />
        </label>
        <br />
        <label>
          Start Date:
          <input type="date" v-model="project.startDate" required />
        </label>
        <br />
        <label>
          End Date:
          <input type="date" v-model="project.endDate" required />
        </label>
        <br />
        <label>
          Priority:
          <input type="number" v-model="project.priority" required />
        </label>
        <br />
        <button type="submit">Next</button>
      </form>
    </div>

    <div v-if="step === 2">
      <h2>Step 2: Companies</h2>
      <form @submit.prevent="goToNextStep">
        <label>
          Client Company:
          <input v-model="project.clientCompany" required />
        </label>
        <br />
        <label>
          Contractor Company:
          <input v-model="project.contractorCompany" required />
        </label>
        <br />
        <button type="submit">Next</button>
      </form>
    </div>

    <div v-if="step === 3">
      <h2>Step 3: Select Project Manager</h2>
      <select v-model="project.projectManagerId" required>
        <option v-for="employee in employees" :value="employee.id" :key="employee.id">
          {{ employee.firstName }} {{ employee.lastName }}
        </option>
      </select>
      <br />
      <button @click="goToNextStep">Next</button>
    </div>

    <div v-if="step === 4">
      <h2>Step 4: Select Employees</h2>
      <div v-for="employee in employees" :key="employee.id">
        <input type="checkbox" :value="employee.id" v-model="selectedEmployees" />
        {{ employee.firstName }} {{ employee.lastName }}
      </div>
      <br />
      <button @click="goToNextStep">Next</button>
    </div>

    <div v-if="step === 5">
      <h2>Step 5: Upload Documents</h2>
      <input type="file" multiple @change="handleFileUpload" />
      <br />
      <button @click="submitProject">Submit</button>
    </div>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        step: 1,
        project: {
          name: '',
          startDate: '',
          endDate: '',
          priority: null,
          clientCompany: '',
          contractorCompany: '',
          projectManagerId: null,
          employees: [],
        },
        employees: [],
        selectedEmployees: [],
        files: [],
      };
    },
    methods: {
      async fetchEmployees() {
        try {
          const response = await fetch('/api/employees'); // API для получения сотрудников
          if (!response.ok) throw new Error('Failed to fetch employees');
          this.employees = await response.json();
        } catch (error) {
          console.error(error);
          alert('Could not load employees');
        }
      },
      goToNextStep() {
        this.step++;
        if (this.step === 3 || this.step === 4) {
          this.fetchEmployees(); // Загружаем сотрудников на этапах 3 и 4
        }
      },
      handleFileUpload(event) {
        this.files = Array.from(event.target.files);
      },
      async submitProject() {
        try {
          // Шаг 1: Создаем проект
          const projectResponse = await fetch('/api/projects', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
              ...this.project,
              employees: this.selectedEmployees,
            }),
          });

          if (!projectResponse.ok) throw new Error('Failed to create project');

          const project = await projectResponse.json();

          // Шаг 2: Загружаем файлы
          if (this.files.length > 0) {
            const formData = new FormData();
            this.files.forEach((file) => {
              formData.append('files', file);
            });

            const uploadResponse = await fetch(
              `/api/projects/${project.id}/upload-documents`,
              {
                method: 'POST',
                body: formData,
              }
            );

            if (!uploadResponse.ok) throw new Error('Failed to upload documents');
          }

          alert('Project created successfully!');
          this.step = 1; // Сбрасываем визард на начало
        } catch (error) {
          console.error(error);
          alert('An error occurred: ' + error.message);
        }
      },
    },
  };
</script>

<style>
  /* Добавьте стили по необходимости */
</style>
