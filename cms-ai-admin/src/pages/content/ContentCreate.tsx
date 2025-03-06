import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useMutation } from 'react-query';
import { 
  Typography, 
  Box, 
  Paper, 
  TextField, 
  Button,
  Stack,
  Alert
} from '@mui/material';
import { Formik, Form, Field } from 'formik';
import * as Yup from 'yup';
import { contentService } from '../../api/contentService';
import { ContentCreateDto } from '../../types/content';

const validationSchema = Yup.object({
  title: Yup.string().required('Title is required').max(200, 'Title must be at most 200 characters'),
  body: Yup.string().required('Content is required'),
});

const initialValues: ContentCreateDto = {
  title: '',
  body: '',
};

const ContentCreate: React.FC = () => {
  const navigate = useNavigate();
  const mutation = useMutation(contentService.create, {
    onSuccess: (data) => {
      navigate(`/content/${data.id}`);
    },
  });

  const handleSubmit = (values: ContentCreateDto) => {
    mutation.mutate(values);
  };

  return (
    <>
      <Typography variant="h4" gutterBottom>Create New Content</Typography>
      <Paper sx={{ p: 3, mb: 3 }}>
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={handleSubmit}
        >
          {({ errors, touched, isSubmitting, values }) => (
            <Form>
              <Stack spacing={3}>
                {mutation.isError && (
                  <Alert severity="error">
                    An error occurred: {(mutation.error as Error).message}
                  </Alert>
                )}

                <Field
                  as={TextField}
                  name="title"
                  label="Title"
                  fullWidth
                  variant="outlined"
                  error={touched.title && Boolean(errors.title)}
                  helperText={touched.title && errors.title}
                />

                <Field
                  as={TextField}
                  name="body"
                  label="Content"
                  fullWidth
                  multiline
                  rows={10}
                  variant="outlined"
                  error={touched.body && Boolean(errors.body)}
                  helperText={touched.body && errors.body}
                />

                <Box sx={{ display: 'flex', justifyContent: 'flex-end', gap: 2 }}>
                  <Button 
                    type="button" 
                    variant="outlined" 
                    onClick={() => navigate('/content')}
                  >
                    Cancel
                  </Button>
                  <Button 
                    type="submit" 
                    variant="contained" 
                    color="primary"
                    disabled={isSubmitting || mutation.isLoading}
                  >
                    {isSubmitting || mutation.isLoading ? 'Creating...' : 'Create'}
                  </Button>
                </Box>
              </Stack>
            </Form>
          )}
        </Formik>
      </Paper>
    </>
  );
};

export default ContentCreate;