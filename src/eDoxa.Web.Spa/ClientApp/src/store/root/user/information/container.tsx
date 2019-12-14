import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserInformations } from "store/actions/identity/actions";
import { RootState } from "store/types";

export const withtUserInformations = (
  HighOrderComponent: FunctionComponent<any>
) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserInformations();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      informations: state.root.user.information
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadUserInformations: () => dispatch(loadUserInformations())
    };
  };

  return connect(mapStateToProps, mapDispatchToProps)(Container);
};
