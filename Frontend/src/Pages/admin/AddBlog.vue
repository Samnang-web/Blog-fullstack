<template>
  <div
    class="flex-1 p-4 ml-4 md:p-10 bg-blue-50/50 overflow-scroll items-center"
  >
    <h1 class="text-3xl font-bold mb-4 ml-10">Add Blogs</h1>
    <form
      @submit.prevent="submitBlog"
      class="bg-blue-50/50 text-gray-600 h-full"
    >
      <div class="bg-white w-full max-w-4xl p-4 md:p-10 sm:m-10 shadow rounded">
        <!-- Image Upload -->
        <div>
          <label class="block text-lg font-medium mb-2">Upload Image</label>
          <input
            type="file"
            ref="fileInputRef"
            accept="image/*"
            @change="onFileChange"
            class="block w-full text-sm text-gray-500 file:mr-4 file:py-2 file:px-4 file:rounded-md file:border-0 file:text-sm file:font-semibold file:bg-blue-100 file:text-blue-700 hover:file:bg-blue-200"
            required
          />
        </div>

        <!-- Blog Title -->
        <div>
          <label class="block text-lg font-medium mb-2 mt-4">Blog Title</label>
          <input
            type="text"
            placeholder="e.g. The World of Vue.js"
            class="w-full max-w-lg mt-2 p-2 border border-gray-300 outline-none rounded focus:outline-none focus:ring-2 focus:ring-blue-300"
            v-model="title"
            required
          />
        </div>

        <!-- Blog Content -->
        <div>
          <label class="block text-lg font-medium mb-2 mt-4">Sub Title</label>
          <textarea
            rows="2"
            placeholder="Brief overview of the blog..."
            class="w-full max-w-lg mt-2 p-2 border border-gray-300 outline-none rounded focus:outline-none focus:ring-2 focus:ring-blue-300"
            v-model="content"
            required
          ></textarea>
        </div>

        <!-- Blog Description -->
        <div>
          <label class="block text-lg font-medium mb-2 mt-4">Description</label>
          <textarea
            rows="8"
            placeholder="Write your full blog description here...."
            class="w-full bg-white px-4 py-3 rounded-md border border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-300"
            v-model="description"
            required
          ></textarea>
        </div>

        <!-- Choose Category -->
        <div class="mt-6">
          <label class="block text-lg font-medium mb-2">Choose Category</label>
          <select
            class="w-full max-w-md px-3 py-2 border border-gray-300 text-gray-600 rounded outline-none focus:ring-2 focus:ring-blue-300"
            v-model="categoryId"
            required
          >
            <option disabled value="">Please select</option>
            <option
              v-for="category in categories"
              :key="category.id"
              :value="category.id"
            >
              {{ category.name }}
            </option>
          </select>
        </div>

        <!-- Author Name -->
        <div>
          <label class="block text-lg font-medium mb-2 mt-4">Author Name</label>
          <input
            type="text"
            placeholder="e.g. Samnang Heang"
            class="w-full max-w-lg mt-2 p-2 border border-gray-300 outline-none rounded focus:outline-none focus:ring-2 focus:ring-blue-300"
            v-model="authorName"
            required
          />
        </div>

        <!-- Submit Button -->
        <div class="pt-4">
          <button
            type="submit"
            class="bg-blue-600 text-white px-6 py-3 rounded-md hover:bg-blue-700 transition-all"
          >
            Submit Blog
          </button>
        </div>
      </div>
    </form>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { addBlog } from "../../Services/BlogService";
import { addCategory, getCategories } from "../../Services/CategoryService";

const title = ref("");
const description = ref("");
const content = ref("");
const categoryId = ref("");
const imageFile = ref(null);
const categories = ref([]);
const authorName = ref("");
const fileInputRef = ref(null);

const onFileChange = (e) => {
  const file = e.target.files[0];
  if (file) imageFile.value = file;
};

const submitBlog = async () => {
  if (
    !title.value ||
    !description.value ||
    !content.value ||
    !categoryId.value ||
    !imageFile.value ||
    !authorName.value
  ) {
    alert("Please fill all required fields");
    return;
  }

  const formData = new FormData();
  formData.append("Title", title.value);
  formData.append("Description", description.value);
  formData.append("Content", content.value);
  formData.append("CategoryId", categoryId.value.toString());
  formData.append("ImageUrl", imageFile.value); // corrected key
  formData.append("AuthorName", authorName.value);

  try {
    await addBlog(formData);
    alert("Blog submitted successfully!");

    // Reset fields
    title.value = "";
    description.value = "";
    content.value = "";
    categoryId.value = "";
    imageFile.value = null;
    authorName.value = "";
    fileInputRef.value.value = ""; // clear file input
  } catch (error) {
    alert(
      "Error submitting blog: " +
        (error.response?.data?.message || error.message)
    );
  }
};

onMounted(async () => {
  try {
    const data = await getCategories();
    categories.value = data;
  } catch (err) {
    alert("Failed to fetch categories: " + err.message);
  }
});
</script>
