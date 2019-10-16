import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserInformations, createUserInformations, updateUserInformations } from "./actions";
import { RootState } from "store/root/types";

export const withtUserInformations = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.actions.loadPersonalInfo();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      informations: state.user.informations.data
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadPersonalInfo: () => dispatch(loadUserInformations()),
        createPersonalInfo: (data: any) => {
          const { year, month, day } = data.birthDate;
          data.birthDate = new Date(year, month, day);
          return dispatch(createUserInformations(data)).then(() => dispatch(loadUserInformations()));
        },
        updatePersonalInfo: (data: any) => dispatch(updateUserInformations(data)).then(() => dispatch(loadUserInformations()))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
