import React, { FunctionComponent, useState } from "react";
import { Badge, Popover, PopoverBody, PopoverHeader } from "reactstrap";
import Balance from "components/User/Balance";
import DepositMoney from "./Deposit";
import WithdrawMoney from "./Withdraw";
import { CURRENCY_MONEY } from "types";

const MoneyBreadcrumb: FunctionComponent<any> = ({ className }) => {
  const [open, setOpen] = useState(false);
  return (
    <div className={className}>
      <Badge
        id="moneyPopover"
        className="bg-gray-900"
        style={{ width: "100px" }}
      >
        <Balance currency={CURRENCY_MONEY} attribute="available" />
      </Badge>
      <Popover
        style={{
          width: "250px"
        }}
        placement="bottom"
        isOpen={open}
        target="moneyPopover"
        trigger="hover"
        delay={{ show: 0, hide: 250 }}
        toggle={() => setOpen(!open)}
      >
        <PopoverHeader className="text-uppercase">MONEY BALANCE</PopoverHeader>
        <PopoverBody>
          <dl className="row mb-0">
            <dt className="col-6">Available</dt>
            <dd className="col-6">
              <Balance
                currency={CURRENCY_MONEY}
                attribute="available"
                alignment="right"
              />
            </dd>
            <dt className="col-6">Pending</dt>
            <dd className="col-6">
              <Balance
                currency={CURRENCY_MONEY}
                attribute="pending"
                alignment="right"
              />
            </dd>
          </dl>
          <DepositMoney />
          <WithdrawMoney />
        </PopoverBody>
      </Popover>
    </div>
  );
};

export default MoneyBreadcrumb;
