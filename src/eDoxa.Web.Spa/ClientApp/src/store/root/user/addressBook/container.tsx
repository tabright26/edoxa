import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserAddressBook } from "store/root/user/addressBook/actions";
import { RootState } from "store/root/types";

export const withUserAddressBook = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadAddressBook();
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
      loadAddressBook: () => dispatch(loadUserAddressBook())
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
