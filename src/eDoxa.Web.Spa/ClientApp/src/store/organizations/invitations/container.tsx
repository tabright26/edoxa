import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadInvitations } from "store/organizations/invitations/actions";
import { AppState } from "store/types";

export const connectInvitations = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, invitations, ...attributes }) => {
    useEffect((): void => {
      actions.loadInvitations();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} invitations={invitations} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      invitations: state.organizations.invitations
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadInvitations: () => dispatch(loadInvitations())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
