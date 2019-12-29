import React, { FunctionComponent, useEffect } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import Badge from "components/Shared/Badge";
import { compose } from "recompose";
import Loading from "components/Shared/Loading";
import { RootState } from "store/types";
import { loadUserEmail } from "store/actions/identity";
import { connect } from "react-redux";

const Email: FunctionComponent<any> = ({
  className,
  email: { data, error, loading },
  loadUserEmail
}) => {
  useEffect((): void => {
    if (data === null) {
      loadUserEmail();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">EMAIL</strong>
        {data && (
          <Badge.Verification
            className="ml-3 my-auto"
            verified={data.verified}
          />
        )}
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : data ? (
          <dl className="row mb-0">
            <dd className="col-sm-3 mb-0 text-muted">Email</dd>
            <dd className="col-sm-9 mb-0">{data.address}</dd>
          </dl>
        ) : (
          <span>Not found</span>
        )}
      </CardBody>
    </Card>
  );
};

const mapStateToProps = (state: RootState) => {
  return {
    email: state.root.user.email
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    loadUserEmail: () => dispatch(loadUserEmail())
  };
};

const enhance = compose<any, any>(connect(mapStateToProps, mapDispatchToProps));

export default enhance(Email);
