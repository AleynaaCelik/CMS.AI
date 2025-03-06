import React, { useState } from 'react';
import { 
  Typography, 
  Box, 
  Paper,
  TextField,
  Button,
  Stack,
  Alert
} from '@mui/material';
import { useParams, useNavigate } from 'react-router-dom';

const ContentEdit: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Mock data - gerçek uygulamada API'den alınır
  const [formData, setFormData] = useState({
    title: 'Sample Content',
    body: 'This is a sample content body. In a real application, this would be fetched from the API.'
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSubmitting(true);
    setError(null);

    try {
      // API update call would go here
      console.log('Updating content with ID:', id);
      console.log('Data:', formData);
      
      // Simulate API call
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      navigate(`/content/${id}`);
    } catch (err) {
      setError('An error occurred while updating the content.');
      console.error(err);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <>
      <Typography variant="h4" gutterBottom>Edit Content</Typography>
      
      <Paper sx={{ p: 3, mb: 3 }}>
        {error && (
          <Alert severity="error" sx={{ mb: 3 }}>
            {error}
          </Alert>
        )}
        
        <form onSubmit={handleSubmit}>
          <Stack spacing={3}>
            <TextField
              name="title"
              label="Title"
              value={formData.title}
              onChange={handleChange}
              fullWidth
              required
            />
            
            <TextField
              name="body"
              label="Content"
              value={formData.body}
              onChange={handleChange}
              multiline
              rows={10}
              fullWidth
              required
            />
            
            <Box sx={{ display: 'flex', justifyContent: 'flex-end', gap: 2 }}>
              <Button 
                type="button" 
                variant="outlined" 
                onClick={() => navigate(`/content/${id}`)}
              >
                Cancel
              </Button>
              <Button 
                type="submit" 
                variant="contained" 
                disabled={isSubmitting}
              >
                {isSubmitting ? 'Saving...' : 'Save Changes'}
              </Button>
            </Box>
          </Stack>
        </form>
      </Paper>
    </>
  );
};

export default ContentEdit;