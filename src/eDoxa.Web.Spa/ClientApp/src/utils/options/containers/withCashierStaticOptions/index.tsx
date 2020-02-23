import React, { FunctionComponent, useEffect } from "react";
import { connect, MapDispatchToProps, MapStateToProps } from "react-redux";
import { compose } from "recompose";
import { RootState } from "store/types";
import { CashierStaticOptions } from "types";
import { loadCashierStaticOptions } from "store/actions/static";

export const withCashierStaticOptions = (
  WrappedComponent: FunctionComponent
) => {
  type OwnProps = {};

  type StateProps = {
    options?: CashierStaticOptions;
  };

  type DispatchProps = {
    loadOptions: () => void;
  };

  type Props = StateProps & DispatchProps;

  const EnhancedComponent: FunctionComponent<Props> = ({
    loadOptions,
    options,
    ...props
  }) => {
    useEffect(() => {
      loadOptions();
    }, [loadOptions]);
    return <WrappedComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<
    StateProps,
    OwnProps,
    RootState
  > = state => {
    return {
      options: state.static.cashier
    };
  };

  const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
    dispatch: any
  ) => {
    return {
      loadOptions: () => {
        dispatch(loadCashierStaticOptions());
      }
    };
  };

  const enhance = compose(connect(mapStateToProps, mapDispatchToProps));

  return enhance(EnhancedComponent);
};
