/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
      primary : {
        DEFAULT: '#5044EF',
          10: '#E8E7FC',  
          40: '#9490F5',  
          100: '#5044EF',  
      }
    },
    },
  },
  plugins: [],
}

