import i18next from 'i18next';
import en from './navigation-i18n/en';
import fr from './navigation-i18n/fr';

i18next.addResourceBundle('en', 'navigation', en);
i18next.addResourceBundle('fr', 'navigation', fr);

const navigationConfig = [
  {
    id: 'home',
    title: 'Home',
    type: 'item',
    icon: 'heroicons-outline:home',
    url: '',
  },
  {
    id: 'divider-1',
    type: 'divider',
  },
  {
    id: 'setup',
    title: 'Setup',
    subtitle: 'Schools, students and staffs...',
    type: 'collapse',
    icon: 'heroicons-outline:cog',
    children: [
      {
        id: 'school_el',
        title: 'Schools',
        type: 'collapse',
        subtitle: 'School types...',
        icons: 'heroicons-outline:star',
        children: [
          {
            id: 'schooltypes',
            title: 'School Types',
            type: 'item',
            url: '/schooltypes',
          },
          {
            id: 'schools',
            title: 'Schools',
            type: 'item',
            url: '/schools',
          },
        ],
      },
      {
        id: 'student_el',
        title: 'Students',
        type: 'collapse',
        subtitle: 'Student types...',
        icons: 'heroicons-outline:star',
        children: [
          {
            id: 'studenttypes',
            title: 'Student Types',
            type: 'item',
            url: '/studenttypes',
          },
          {
            id: 'students',
            title: 'Students',
            type: 'item',
            url: '/students',
          },
        ],
      },
      {
        id: 'staff_el',
        title: 'Staffs',
        type: 'collapse',
        subtitle: 'Staff types...',
        icons: 'heroicons-outline:star',
        children: [
          {
            id: 'stafftypes',
            title: 'Staff Types',
            type: 'item',
            url: 'stafftypes',
          },
          {
            id: 'staffs',
            title: 'Staffs',
            type: 'item',
            url: 'staffs',
          },
        ],
      },
    ],
  },
];

export default navigationConfig;
