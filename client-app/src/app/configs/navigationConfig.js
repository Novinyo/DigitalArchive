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
            url: '/setup/schooltypes',
          },
          {
            id: 'schools',
            title: 'Schools',
            type: 'item',
            url: '/setup/schools',
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
          // {
          //   id: 'studenttypes',
          //   title: 'Student Types',
          //   type: 'item',
          //   url: '/setup/studenttypes',
          // },
          {
            id: 'students',
            title: 'Students',
            type: 'item',
            url: '/setup/students',
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
            url: '/setup/stafftypes',
          },
          {
            id: 'staffs',
            title: 'Staffs',
            type: 'item',
            url: '/setup/contacts',
          },
        ],
      },
      {
        id: 'docType_el',
        title: 'Documents',
        type: 'collapse',
        subtitle: 'Document types...',
        icons: 'heroicons-outline:star',
        children: [
          {
            id: 'documenttypes',
            title: 'Document Types',
            type: 'item',
            url: '/setup/documenttypes',
          },
          // {
          //   id: 'staffs',
          //   title: 'Staffs',
          //   type: 'item',
          //   url: '/setup/contacts',
          // },
        ],
      },
    ],
  },
];

export default navigationConfig;
