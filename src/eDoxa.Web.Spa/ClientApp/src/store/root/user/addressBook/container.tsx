import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { CREATE_USER_ADDRESS_MODAL } from "modals";
import { loadUserAddressBook, createUserAddress, updateUserAddress, deleteUserAddress } from "store/root/user/addressBook/actions";
import { RootState } from "store/root/types";

export const withUserAddressBook = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, addressBook, ...attributes }) => {
    useEffect((): void => {
      actions.loadAddressBook();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent actions={actions} addressBook={addressBook} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      addressBook: state.user.addressBook.data
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadAddressBook: () => dispatch(loadUserAddressBook()),
        addAddress: (data: any) => dispatch(createUserAddress(data)).then(() => dispatch(loadUserAddressBook())),
        updateAddress: (addressId: string, data: any) => dispatch(updateUserAddress(addressId, data)).then(() => dispatch(loadUserAddressBook())),
        removeAddress: (addressId: string) => dispatch(deleteUserAddress(addressId)),
        showCreateAddressModal: () => dispatch(show(CREATE_USER_ADDRESS_MODAL))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
