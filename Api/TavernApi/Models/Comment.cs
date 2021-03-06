﻿using System;
using System.Collections.Generic;
using System.Linq;
using TavernApi.Models.Identity;

namespace TavernApi.Models
{
  public class Comment
  {
    public long Id { get; set; }
    public string Content { get; set; }
    public long ProjectId { get; set; }
    public virtual CommentNode Parent { get; set; }
    public virtual IEnumerable<CommentNode> Children { get; set; }
    public virtual User Creator { get; set; }
    public DateTime CreationTimeStamp { get; set; }

  }

  public class CommentNode
  {
    public long ParentId { get; set; }
    public virtual Comment Parent { get; set; }

    public long ChildId { get; set; }
    public virtual Comment Child { get; set; }
  }

  public class CommentDTO
  {
    public long Id { get; set; }
    public string Content { get; set; }
    public long ProjectId { get; set; }
    public long? ParentId { get; set; }
    public IEnumerable<CommentDTO> Children { get; set; }
    public UserDTO Creator { get; set; }
    public DateTime CreationTimeStamp { get; set; }

    public CommentDTO(Comment comment, long remainingDepth) 
      : this(comment, remainingDepth, null)
    {}
    public CommentDTO(Comment comment, long remainingDepth, long? parentId)
    {
      Id = comment.Id;
      Content = comment.Content;
      ProjectId = comment.ProjectId;
      ParentId = parentId;
      Children = (comment.Children != null && remainingDepth > 0) ? comment.Children.Select(n => new CommentDTO(n.Child, remainingDepth--, Id)) : null;
      Creator = new UserDTO(comment.Creator);
      CreationTimeStamp = comment.CreationTimeStamp;
    }
  }

  public class CommentDCO
  {
    public long ProjectId { get; set; }
    public long? ParentId { get; set; }
    public string Content { get; set; }
  }
}
