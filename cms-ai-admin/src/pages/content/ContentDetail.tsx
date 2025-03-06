import React from 'react';
import { 
  Typography, 
  Box, 
  Paper,
  Button,
  Chip,
  Divider,
  Stack,
  Card,
  CardContent
} from '@mui/material';
import { useParams, Link, useNavigate } from 'react-router-dom';

const ContentDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  // Mock data - gerçek uygulamada API'den alınır
  const content = {
    id: id,
    title: 'Sample Content',
    body: 'This is a sample content body. In a real application, this would be fetched from the API.',
    status: 'Draft',
    createdAt: new Date().toISOString(),
    version: 1
  };

  const handleDelete = () => {
    if (window.confirm('Are you sure you want to delete this content?')) {
      // API delete call would go here
      navigate('/content');
    }
  };

  return (
    <>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4" gutterBottom>View Content</Typography>
        <Box sx={{ display: 'flex', gap: 2 }}>
          <Button 
            variant="outlined" 
            component={Link} 
            to={`/content/${id}/edit`}
          >
            Edit
          </Button>
          <Button 
            variant="outlined" 
            color="error" 
            onClick={handleDelete}
          >
            Delete
          </Button>
          <Button 
            variant="outlined" 
            component={Link} 
            to="/content"
          >
            Back to List
          </Button>
        </Box>
      </Box>

      <Paper sx={{ p: 3, mb: 3 }}>
        <Stack spacing={2}>
          <Box>
            <Typography variant="subtitle1" fontWeight="bold">Title</Typography>
            <Typography variant="body1">{content.title}</Typography>
          </Box>
          
          <Divider />
          
          <Box>
            <Typography variant="subtitle1" fontWeight="bold">Status</Typography>
            <Chip 
              label={content.status} 
              color={content.status === 'Published' ? 'success' : 'default'} 
              size="small" 
            />
          </Box>
          
          <Divider />
          
          <Box>
            <Typography variant="subtitle1" fontWeight="bold">Content</Typography>
            <Typography variant="body1" sx={{ whiteSpace: 'pre-wrap' }}>
              {content.body}
            </Typography>
          </Box>
          
          <Divider />
          
          <Box>
            <Typography variant="subtitle1" fontWeight="bold">Metadata</Typography>
            <Typography variant="body2">
              Created: {new Date(content.createdAt).toLocaleString()}<br />
              Version: {content.version}
            </Typography>
          </Box>
        </Stack>
      </Paper>

      <Card sx={{ mb: 3 }}>
        <CardContent>
          <Typography variant="h6" gutterBottom>AI Analysis</Typography>
          <Button variant="contained" color="secondary">
            Analyze with AI
          </Button>
        </CardContent>
      </Card>
    </>
  );
};

export default ContentDetail;