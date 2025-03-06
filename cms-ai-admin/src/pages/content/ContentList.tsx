import React from 'react';
import { useQuery } from 'react-query';
import { 
  Typography, 
  Button, 
  Box, 
  Paper, 
  Table, 
  TableBody, 
  TableCell, 
  TableContainer, 
  TableHead, 
  TableRow,
  Chip,
  IconButton,
  CircularProgress
} from '@mui/material';
import { Link } from 'react-router-dom';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import VisibilityIcon from '@mui/icons-material/Visibility';
import { contentService } from '../../api/contentService';
import { Content, ContentStatus } from '../../types/content';

const ContentList: React.FC = () => {
  const { data, isLoading, error, refetch } = useQuery('contents', contentService.getAll);

  const getStatusChip = (status: ContentStatus) => {
    switch (status) {
      case ContentStatus.Draft:
        return <Chip label="Draft" color="default" size="small" />;
      case ContentStatus.Published:
        return <Chip label="Published" color="success" size="small" />;
      case ContentStatus.Archived:
        return <Chip label="Archived" color="error" size="small" />;
      case ContentStatus.UnderReview:
        return <Chip label="Under Review" color="warning" size="small" />;
      default:
        return <Chip label="Unknown" color="default" size="small" />;
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('Are you sure you want to delete this content?')) {
      try {
        await contentService.delete(id);
        refetch();
      } catch (error) {
        console.error('Error deleting content:', error);
      }
    }
  };

  if (isLoading) {
    return <CircularProgress />;
  }

  if (error) {
    return <Typography color="error">Error loading content: {(error as Error).message}</Typography>;
  }

  return (
    <>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" gutterBottom>Content</Typography>
        <Button 
          variant="contained" 
          color="primary" 
          component={Link} 
          to="/content/create"
          startIcon={<AddIcon />}
        >
          Add New
        </Button>
      </Box>

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Title</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Last Updated</TableCell>
              <TableCell>Version</TableCell>
              <TableCell align="right">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {data?.map((content: Content) => (
              <TableRow key={content.id}>
                <TableCell>{content.title}</TableCell>
                <TableCell>{getStatusChip(content.status)}</TableCell>
                <TableCell>
                  {content.lastModifiedAt 
                    ? new Date(content.lastModifiedAt).toLocaleDateString() 
                    : new Date(content.createdAt).toLocaleDateString()}
                </TableCell>
                <TableCell>{content.version}</TableCell>
                <TableCell align="right">
                  <IconButton component={Link} to={`/content/${content.id}`}>
                    <VisibilityIcon />
                  </IconButton>
                  <IconButton component={Link} to={`/content/${content.id}/edit`}>
                    <EditIcon />
                  </IconButton>
                  <IconButton color="error" onClick={() => handleDelete(content.id)}>
                    <DeleteIcon />
                  </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
};

export default ContentList;