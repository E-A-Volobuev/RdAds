<template>
  <div class="">
    <div v-if="isEdit === 'true'">
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Сфера деятельности:"
            v-model="ad.sphereOfActivity"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="График работы:"
            v-model="ad.workingSchedule"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Опыт работы: "
            v-model="ad.workExperience"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="График выплаты:"
            v-model="ad.frequencyOfPayments"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-combobox
            v-model="ad.sex"
            :items="sex"
            label="Пол:"
            outlined
            dense
          ></v-combobox>
        </v-col>
      </v-row>
    </div>
    <div v-else>
      <div class="text--primary">
        Сфера деятельности: {{ ad.sphereOfActivity }}
      </div>
      <div class="text--primary">График работы: {{ ad.workingSchedule }}</div>
      <div class="text--primary">Опыт работы: {{ ad.workExperience }}</div>
      <div class="text--primary">
        График выплаты: {{ ad.frequencyOfPayments }}
      </div>
      <div class="text--primary">Пол: {{ ad.sex }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "work",
  props: ["ad","isEdit","sex"],
  data: () => ({}),
  methods: {},
};
</script>