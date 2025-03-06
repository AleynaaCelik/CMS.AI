export interface Content {
    id: string;
    title: string;
    slug: string;
    body: string;
    status: ContentStatus;
    version: number;
    createdAt: string;
    createdBy: string;
    lastModifiedAt?: string;
    lastModifiedBy?: string;
    metaData: ContentMeta[];
  }
  
  export enum ContentStatus {
    Draft = 0,
    Published = 1,
    Archived = 2,
    UnderReview = 3
  }
  
  export interface ContentMeta {
    id: string;
    contentId: string;
    language: string;
    key: string;
    value: string;
  }
  
  export interface ContentCreateDto {
    title: string;
    body: string;
  }
  
  export interface ContentUpdateDto {
    title: string;
    body: string;
  }
  
  export interface AIAnalysisResult {
    title: string;
    description: string;
    keywords: string[];
    improvedContent: string;
    imagePrompt: string;
    sentimentScore: number;
    summary: string;
  }