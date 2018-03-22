namespace RAM.Domain.Helpers
{
    public enum Instruction
    {
        LOAD,
        STORE,
        READ,
        WRITE,
        ADD,
        SUB,
        MULT,
        DIV,
        MOD,
        JUMP,
        JZERO,
        JGTZ,
        JLTZ,
        HALT,
        Empty
    }
}