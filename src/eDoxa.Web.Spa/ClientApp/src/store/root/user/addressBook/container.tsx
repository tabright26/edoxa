import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { CREATE_ADDRESS_MODAL } from "modals";
import { loadAddressBook, addAddress, updateAddress, removeAddress } from "store/root/user/addressBook/actions";
import { RootState } from "store/root/types";

export const withUserAddressBook = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, addressBook, ...attributes }) => {
    useEffect((): void => {
      actions.loadAddressBook();
         // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} addressBook={addressBook} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      addressBook: state.user.addressBook
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadAddressBook: () => dispatch(loadAddressBook()),
        addAddress: (data: any) => dispatch(addAddress(data)).then(() => dispatch(loadAddressBook())),
        updateAddress: (addressId: string, data: any) => dispatch(updateAddress(addressId, data)).then(() => dispatch(loadAddressBook())),
        removeAddress: (addressId: string) => dispatch(removeAddress(addressId)),
        showCreateAddressModal: () => dispatch(show(CREATE_ADDRESS_MODAL))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
