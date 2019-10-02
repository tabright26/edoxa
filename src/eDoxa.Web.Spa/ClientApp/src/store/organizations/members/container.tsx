import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadMembers } from "store/organizations/members/actions";
import { AppState } from "store/types";

export const connectMembers = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, members, ...attributes }) => {
    useEffect((): void => {
      actions.loadMembers();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} members={members} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      members: state.organizations.members
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadMembers: () => dispatch(loadMembers())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
