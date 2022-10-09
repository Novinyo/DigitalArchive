import useThemeMediaQuery from '@fuse/hooks/useThemeMediaQuery';
import FusePageCarded from '@fuse/core/FusePageCarded';
import withReducer from 'app/store/withReducer';
import reducer from '../../store';
import DocumentTypesHeader from './DocumentTypesHeader';
import DocumentTypesTable from './DocumentTypesTable';

function DocumentTypes() {
  const isMobile = useThemeMediaQuery((theme) => theme.breakpoints.down('lg'));
  return (
    <FusePageCarded
      header={<DocumentTypesHeader />}
      content={<DocumentTypesTable />}
      scroll={isMobile ? 'normal' : 'content'}
    />
  );
}

export default withReducer('setupApp', reducer)(DocumentTypes);
