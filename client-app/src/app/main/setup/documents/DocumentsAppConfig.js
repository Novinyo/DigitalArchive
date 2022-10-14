import { lazy } from 'react';

const DocumentType = lazy(() => import('./type/DocumentType'));
const DocumentTypes = lazy(() => import('./type/DocumentTypes'));

const DocumentsAppConfig = {
  settings: {
    layout: {},
  },
  routes: [
    {
      path: 'setup/documenttypes',
      element: <DocumentTypes />,
    },
    {
      path: 'setup/documenttypes/:documentTypeId/*',
      element: <DocumentType />,
    },
  ],
};

export default DocumentsAppConfig;
