import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'node:path'
import autoprefixer from 'autoprefixer'

export default defineConfig(({ mode }) => {
  return {
    //NoteBase
    //base: './',
    //root: 'wwwroot', // Where your static files will be served from
    //build: {
    //  outDir: '../wwwroot/dist', // Output directory for built files
    //},

    base: './',
    build: {
      outDir: 'build',
    },

    //new
    //root: 'ClientApp', // Ensure this points to your ClientApp directory  
    //base: '/ClientApp/public', // Ensure the base points where your app will be hosted  
    //plugins: [react()],
    //build: {
    //  outDir: '../ClientApp', // Adjust this if you're outputting to wwwroot directory
    //}, 
    
    css: {
      preprocessorOptions: {
        scss: {
          silenceDeprecations: ["legacy-js-api"],
        },
      },
      postcss: {
        plugins: [
          autoprefixer({}), // add options if needed
        ],
      },
    },
    esbuild: {
      loader: 'jsx',
      include: /src\/.*\.jsx?$/,
      exclude: [],
    },
    optimizeDeps: {
      force: true,
      esbuildOptions: {
        loader: {
          '.js': 'jsx',
        },
      },
    },
    plugins: [react()],
    resolve: {
      alias: [
        {
          find: 'src/',
          replacement: `${path.resolve(__dirname, 'src')}/`,
        },
      ],
      extensions: ['.mjs', '.js', '.ts', '.jsx', '.tsx', '.json', '.scss'],
    },
    server: {
      port: 3000,
      proxy: {
        // https://vitejs.dev/config/server-options.html
      },
    },
  }
})
