export interface Blog {
  id: number
  title: string
  description: string
  content: string
  imageUrl: string
  categoryName?: string
  authorName: string
  createdAt: string
  updatedAt?: string

}