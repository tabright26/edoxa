import React, { FunctionComponent, useEffect } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import Badge from "components/Shared/Badge";
import { compose } from "recompose";
import { Loading } from "components/Shared/Loading";
import { RootState } from "store/types";
import { loadUserEmail } from "store/actions/identity";
import { connect, MapDispatchToProps, MapStateToProps } from "react-redux";
import { Email } from "types/identity";

type OwnProps = {};

type StateProps = {
  email?: Email;
  loading: boolean;
};

type DispatchProps = {
  loadUserEmail: () => void;
};

type InnerProps = StateProps & DispatchProps;

type OutterProps = {
  className?: string;
};

type Props = InnerProps & OutterProps;

const Panel: FunctionComponent<Props> = ({
  className,
  email,
  loading,
  loadUserEmail
}) => {
  useEffect((): void => {
    if (email === null) {
      loadUserEmail();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">EMAIL</strong>
        {email && (
          <Badge.Verified className="ml-3 my-auto" verified={email.verified} />
        )}
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : email ? (
          <dl className="row mb-0">
            <dd className="col-sm-3 mb-0 text-muted">Email</dd>
            <dd className="col-sm-9 mb-0">{email.address}</dd>
          </dl>
        ) : (
          <span>Not found</span>
        )}
      </CardBody>
    </Card>
  );
};

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    email: state.root.user.email.data,
    loading: state.root.user.email.loading
  };
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any
) => {
  return {
    loadUserEmail: () => dispatch(loadUserEmail())
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps, mapDispatchToProps)
);

export default enhance(Panel);
