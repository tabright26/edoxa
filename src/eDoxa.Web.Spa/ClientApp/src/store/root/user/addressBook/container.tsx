import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { CREATE_USER_ADDRESS_MODAL } from "modals";
import { loadUserAddressBook } from "store/root/user/addressBook/actions";
import { RootState } from "store/root/types";

export const withUserAddressBook = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.actions.loadAddressBook();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      addressBook: state.user.addressBook
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadAddressBook: () => dispatch(loadUserAddressBook()),
        showCreateAddressModal: () => dispatch(show(CREATE_USER_ADDRESS_MODAL))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
