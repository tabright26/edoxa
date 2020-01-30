import React, { FunctionComponent } from "react";
import ReactPaginate from "react-paginate";

interface Props {
  className?: string;
  pageSize?: number;
  totalItems: number;
  onPageChange: (currentPage: number, pageSize: number) => void;
}

export const Paginate: FunctionComponent<Props> = ({
  pageSize = 5,
  totalItems,
  onPageChange,
  className
}) => {
  const pageCount: number = Math.ceil(totalItems / pageSize);
  return (
    <ReactPaginate
      pageCount={pageCount}
      pageRangeDisplayed={pageCount}
      marginPagesDisplayed={pageCount}
      onPageChange={(selectedItem: { selected: number }) =>
        onPageChange(selectedItem.selected, pageSize)
      }
      containerClassName={`pagination ${className}`}
      pageClassName="page-item"
      pageLinkClassName="page-link"
      disabledClassName="disabled"
      activeClassName="active"
      previousClassName="page-item"
      nextClassName="page-item"
      previousLinkClassName="page-link"
      nextLinkClassName="page-link"
      previousLabel="previous"
      nextLabel="next"
      breakLabel="..."
    />
  );
};
