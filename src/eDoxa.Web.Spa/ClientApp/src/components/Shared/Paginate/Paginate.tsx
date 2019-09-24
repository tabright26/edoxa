import React, { FunctionComponent } from "react";
import ReactPaginate from "react-paginate";

interface PaginateProps {
  pageSize?: number;
  totalItems: number;
  onPageChange: (currentPage: number, pageSize: number) => void;
}

const Paginate: FunctionComponent<PaginateProps> = ({ pageSize = 5, totalItems, onPageChange }) => {
  const pageCount: number = Math.ceil(totalItems / pageSize);
  return (
    <ReactPaginate
      pageCount={pageCount}
      pageRangeDisplayed={pageCount}
      marginPagesDisplayed={pageCount}
      onPageChange={(selectedItem: { selected: number }) => onPageChange(selectedItem.selected, pageSize)}
      containerClassName="pagination"
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

export default Paginate;
